using TinyMath.SyntaxNodes;

namespace TinyMath
{
	/// <summary>
	/// Parser class used for processing input into output using tokens.
	/// </summary>
	public class SyntaxParser
	{
		/// <summary>
		/// Creates a new instance of the <see cref="SyntaxParser"/> class.
		/// </summary>
		/// <param name="Input">Source input to be proccessed.</param>
		public SyntaxParser(string Input)
		{
			// Create list for tokens.
			List<SyntaxToken> Tokens = new();

			// Create lexer instance.
			SyntaxLexer Lexer = new(Input);

			// Collect all tokens until lexer is at EOF.
			while (!Lexer.IsEOF())
			{
				// Get next available token.
				SyntaxToken Token = Lexer.NextToken();

				// Check token for defects.
				if (Token.Kind == SyntaxKind.Invalid)
				{
					continue;
				}

				// Add the token to the list.
				Tokens.Add(Token);
			}

			// Finalize tokenized output.
			SyntaxTokens = Tokens.ToArray();
		}

		#region Methods

		/// <summary>
		/// Evaluate an input math equation.
		/// </summary>
		/// <param name="Input">Input math expression.</param>
		/// <returns>The solved expression as a double value.</returns>
		public static double Evaluate(string Input)
		{
			try
			{
				// Create a parser instance.
				SyntaxParser Parser = new(Input);

				// Get final value and return it.
				return Evaluate(Parser.Parse());
			}
			catch (Exception E)
			{
				Console.WriteLine(E.Message);
				return 0.0;
			}
		}

		/// <summary>
		/// Private method to evaluate all sub-nodes.
		/// </summary>
		/// <param name="Node">The previous node.</param>
		/// <param name="LastValue">The previous existing value.</param>
		/// <returns>The final value of all sub-nodes in the input node.</returns>
		private static double Evaluate(SyntaxNode Node, double LastValue = 0.0)
		{
			// Check if the node is actualy an operator.
			if (Node is OperatorNode T)
			{
				LastValue += double.Parse(T.Solve());
			}

			// Get all sub-values.
			foreach (SyntaxNode Child in Node.GetChildren())
			{
				Evaluate(Child, LastValue);
			}

			return LastValue;
		}

		/// <summary>
		/// Matches and returns a token. If the specified kind matches the kind of the current token,
		/// returns the next token. Else, returns new token of specified type.
		/// </summary>
		/// <param name="Kind">The kind of token to request.</param>
		/// <returns>A single token.</returns>
		private SyntaxToken MatchToken(SyntaxKind Kind)
		{
			if (GetToken().Kind == Kind)
			{
				return NextToken();
			}

			// Generate and return new token of requested type.
			return new SyntaxToken(string.Empty, GetToken().Index, Kind);
		}

		/// <summary>
		/// Gets the next token from the token array at current index, and increments the index by one.
		/// </summary>
		/// <returns>A single token from the lexer.</returns>
		private SyntaxToken NextToken()
		{
			// Check if the current index is outside of the input bounds.
			if (IsEOF())
			{
				return SyntaxTokens[^1];
			}

			return SyntaxTokens[Index++];
		}

		/// <summary>
		/// Gets the current token from the token array.
		/// </summary>
		/// <returns>A single token from the lexer.</returns>
		private SyntaxToken GetToken()
		{
			// Check if the current index is outside of the array bounds.
			if (IsEOF())
			{
				return SyntaxTokens[^1];
			}

			return SyntaxTokens[Index];
		}

		/// <summary>
		/// Gets the primary node in the input expression.
		/// </summary>
		/// <returns>A single node.</returns>
		private SyntaxNode GetPrimaryNode()
		{
			// Get a number token and return it inside of a integier node.
			return new IntegierNode(MatchToken(SyntaxKind.Integier));
		}

		/// <summary>
		/// Gets the root node of the input from the lexer.
		/// </summary>
		/// <returns>The root node from the input.</returns>
		public SyntaxNode Parse()
		{
			// Create new SyntaxNode to represent the left side of the expression.
			SyntaxNode Left = GetPrimaryNode();

			while (GetToken().Kind == SyntaxKind.Operator)
			{
				// Get the operator of the expression.
				SyntaxToken OperatorToken =  NextToken();

				// Get the SyntaxNode that represents the right side of the expression.
				SyntaxNode Right = GetPrimaryNode();

				// Set the left size of the expression to this single expression.
				Left = new SyntaxNodes.OperatorNode(Left, OperatorToken, Right);
			}

			// Return the single expression node.
			return Left;
		}

		/// <summary>
		/// Get the EOF state of the parser.
		/// </summary>
		/// <returns>The EOF (end of file) state of the parser.</returns>
		public bool IsEOF()
		{
			return (int)Index >= SyntaxTokens.Length;
		}

		#endregion

		#region Fields

		private readonly SyntaxToken[] SyntaxTokens;
		private ulong Index;

		#endregion
	}
}