namespace TinyMath
{
	/// <summary>
	/// The base syntax node class.
	/// </summary>
	public abstract class SyntaxNode
	{
		#region Properties

		/// <summary>
		/// The kind of node that is defined.
		/// </summary>
		public abstract SyntaxKind Kind { get; }

		#endregion

		#region Methods

		/// <summary>
		/// Gets the children of this node.
		/// </summary>
		public abstract IEnumerable<SyntaxNode> GetChildren();

		#endregion
	}
}