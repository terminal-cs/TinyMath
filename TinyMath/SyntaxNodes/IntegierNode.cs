namespace TinyMath.SyntaxNodes
{
	/// <summary>
	/// The class used to represent integies as a node.
	/// </summary>
	public sealed class IntegierNode : SyntaxNode
	{
		/// <summary>
		/// Creates a new instance of the <see cref="IntegierNode"/> class.
		/// </summary>
		/// <param name="Token">The token of the integier node.</param>
		public IntegierNode(SyntaxToken Token)
		{
			this.Token = Token;
		}

		#region Properties

		/// <summary>
		/// The kind of node that is defined.
		/// </summary>
		public override SyntaxKind Kind => SyntaxKind.Integier;

		#endregion

		#region Methods

		/// <summary>
		/// Gets the children of this node.
		/// </summary>
		public override IEnumerable<SyntaxNode> GetChildren()
		{
			yield return Token;
		}

		#endregion

		#region Fields

		public readonly SyntaxToken Token;

		#endregion
	}
}