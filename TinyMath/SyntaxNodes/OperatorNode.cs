namespace TinyMath.SyntaxNodes
{
	/// <summary>
	/// The class used to represent operators as a node.
	/// </summary>
	public sealed class OperatorNode : SyntaxNode
	{
		/// <summary>
		/// Creates a new instance of the <see cref="OperatorNode"/> class.
		/// </summary>
		/// <param name="Left">The left-hand side of the expression.</param>
		/// <param name="Operator">The operator of the expression.</param>
		/// <param name="Right">The right-hand side of the expression.</param>
		public OperatorNode(SyntaxNode Left, SyntaxToken Operator, SyntaxNode Right)
		{
			this.Left = Left;
			this.Operator = Operator;
			this.Right = Right;
		}

		#region Properties

		/// <summary>
		/// The kind of node that is defined.
		/// </summary>
		public override SyntaxKind Kind => SyntaxKind.Operator;

		#endregion

		#region Methods

		/// <summary>
		/// Gets the children of this node.
		/// </summary>
		public override IEnumerable<SyntaxNode> GetChildren()
		{
			yield return Left;
			yield return Operator;
			yield return Right;
		}

		/// <summary>
		/// Solve the expression for this operator node.
		/// </summary>
		/// <returns>The value of the expression.</returns>
		/// <exception cref="ArgumentException">Thrown on invalid operator detected. Should be impossible.</exception>
		public string Solve()
		{
			// Initialize the value.
			double RightDouble = 0.0;
			double LeftDouble = 0.0;

			// Get the raw number value from the right token.
			if (Right is OperatorNode TRightOperator)
			{
				RightDouble += double.Parse(TRightOperator.Solve());
			}
			if (Right is IntegierNode TRightIntergier)
			{
				if (double.TryParse(TRightIntergier.Token.Value, out double RDValue))
				{
					RightDouble = RDValue;
				}
				if (long.TryParse(TRightIntergier.Token.Value, out long RLValue))
				{
					RightDouble = RLValue;
				}
			}

			// Get the raw number value from the left token.
			if (Left is OperatorNode TLeftOperator)
			{
				LeftDouble += double.Parse(TLeftOperator.Solve());
			}
			if (Left is IntegierNode TLeftIntegier)
			{
				if (double.TryParse(TLeftIntegier.Token.Value, out double LDValue))
				{
					LeftDouble = LDValue;
				}
				if (long.TryParse(TLeftIntegier.Token.Value, out long LLValue))
				{
					LeftDouble = LLValue;
				}
			}
			
			// Do the correct operation and return.
			return Operator.Value switch
			{
				"+" => (LeftDouble + RightDouble).ToString(),
				"-" => (LeftDouble - RightDouble).ToString(),
				"*" => (LeftDouble * RightDouble).ToString(),
				"/" => (LeftDouble / RightDouble).ToString(),
				"^" => Math.Pow(LeftDouble, RightDouble).ToString(),
				"%" => (LeftDouble % RightDouble).ToString(),
				_ => throw new ArgumentException($"Invalid operator '{Operator.Value}' at column {Operator.Index}!"),
			};
		}

		#endregion

		#region Fields

		public SyntaxToken Operator;
		public SyntaxNode Right;
		public SyntaxNode Left;

		#endregion
	}
}