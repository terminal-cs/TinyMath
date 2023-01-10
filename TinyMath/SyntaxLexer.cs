namespace TinyMath
{
	/// <summary>
	/// Lexer class for processing raw input into tokens.
	/// </summary>
	public class SyntaxLexer
	{
		/// <summary>
		/// Creates a new instance of the <see cref="SyntaxLexer"/> class.
		/// </summary>
		/// <param name="Input">Source input to be proccessed.</param>
		public SyntaxLexer(string Input)
		{
			// Initialize all fields.
			Buffer = string.Empty;
			this.Input = Input;
			Index = 0;
		}

		#region Methods

		/// <summary>
		/// Gets the next token from the source from the current index.
		/// </summary>
		/// <returns>The token following the last requested token.</returns>
		public SyntaxToken NextToken()
		{
			// Skip all whitespace characters.
			while (char.IsWhiteSpace(GetChar()))
			{
				// Check if the lexer is at the end of the input.
				if (IsEOF())
				{
					return SyntaxToken.Empty;
				}

				// Incriment the input index by one.
				Index++;
			}

			// Check if the current char is a basic operator.
			if (char.IsDigit(GetChar()))
			{
				// Initialize the buffer.
				Buffer = string.Empty;

				// Concatinate all digits into the buffer.
				while (char.IsDigit(GetChar()) || GetChar() == '.')
				{
					Buffer += NextChar();
				}

				// Send digit buffer to SyntaxToken to be processed.
				return new(Buffer, Index - 1);
			}

			// Create and return token with basic operators.
			return new(NextChar().ToString(), Index - 1);
		}

		/// <summary>
		/// Gets the char at the current index of the input, then increment the index.
		/// </summary>
		/// <returns>A single char from the input at the current index.</returns>
		private char NextChar()
		{
			// Check if the current index is outside of the input bounds.
			if (IsEOF())
			{
				return '\0';
			}

			return Input[(int)Index++];
		}

		/// <summary>
		/// Gets the char at the current index of the input.
		/// </summary>
		/// <returns>A single char from the input at the current index.</returns>
		private char GetChar()
		{
			// Check if the current index is outside of the input bounds.
			if (IsEOF())
			{
				return '\0';
			}

			return Input[(int)Index];
		}

		/// <summary>
		/// Gets the EOF state of the lexer, no operations should continue if EOF occurs.
		/// </summary>
		/// <returns>State of EOF in the lexer.</returns>
		public bool IsEOF()
		{
			return (int)Index >= Input.Length;
		}

		#endregion

		#region Fields

		private readonly string Input;
		private string Buffer;
		private ulong Index;

		#endregion
	}
}