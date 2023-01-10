namespace TinyMath
{
	/// <summary>
	/// The class used to store tokens from input source.
	/// </summary>
	public class SyntaxToken : SyntaxNode
	{
		/// <summary>
		/// Creates a new instance of the <see cref="SyntaxToken"/> class.
		/// </summary>
		/// <param name="Value">Value of the token.</param>
		/// <param name="Index">Index of the token.</param>
		/// <param name="Kind">Kind of the token.</param>
		public SyntaxToken(string Value, ulong Index, SyntaxKind Kind)
		{
			// Initialize all fields.
			this.Value = Value;
			this.Index = Index;
			this.Kind = Kind;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="SyntaxToken"/> class.
		/// </summary>
		/// <param name="Value">Value of the token.</param>
		/// <param name="Index">Index of the token.</param>
		public SyntaxToken(string Value, ulong Index)
		{
			// Initialize all fields.
			this.Value = Value;
			this.Index = Index;

			// Check if input could be a number.\
			if (double.TryParse(Value, out _))
			{
				Kind = SyntaxKind.Integier;
				return;
			}
			if (long.TryParse(Value, out _))
			{
				Kind = SyntaxKind.Integier;
				return;
			}

			// Assign kind to what type of syntax was put in, skip if it's already an integier.
			Kind = Value switch
			{
				"+" => SyntaxKind.Operator,
				"-" => SyntaxKind.Operator,
				"*" => SyntaxKind.Operator,
				"/" => SyntaxKind.Operator,
				"^" => SyntaxKind.Operator,
				"%" => SyntaxKind.Operator,
				_ => throw new ArgumentException($"Invalid token kind '{Value}'!"),
			};
		}

		#region Properties

		/// <summary>
		/// Empty instance of the <see cref="SyntaxToken"/> class.
		/// </summary>
		public static SyntaxToken Empty => new(string.Empty, 0);

		/// <summary>
		/// The kind of node that is defined.
		/// </summary>
		public override SyntaxKind Kind { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Gets the children of this node.
		/// </summary>
		public override IEnumerable<SyntaxNode> GetChildren()
		{
			return Enumerable.Empty<SyntaxNode>();
		}

		#endregion

		#region Fields

		public readonly string Value;
		public readonly ulong Index;

		#endregion
	}
}