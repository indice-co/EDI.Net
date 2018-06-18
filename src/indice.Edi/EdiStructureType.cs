using indice.Edi.Serialization;

namespace indice.Edi
{
    /// <summary>
    /// Indicates the container type of an <see cref="EdiStructure"/>. <seealso cref="EdiStructureType"/> 
    /// has values in an ascending order from the outer-most container to the inner-most in order to establish hierarchy.
    /// </summary>
    public enum EdiStructureType
    {
        /// <summary>
        /// No container.
        /// </summary>
        None = 0,

        /// <summary>
        /// Used to indicate <see cref="Interchange"/>
        /// </summary>
        Interchange = 1,

        /// <summary>
        /// Used to indicate <see cref="Group"/>
        /// </summary>
        Group = 2,

        /// <summary>
        /// Used to indicate <see cref="Message"/>
        /// </summary>
        Message = 3,

        /// <summary>
        /// Used to indicate <see cref="SegmentGroup"/>
        /// </summary>
        SegmentGroup = 4,

        /// <summary>
        /// Used to indicate <see cref="Segment"/>
        /// </summary>
        Segment = 5,

        /// <summary>
        /// Used to indicate <see cref="Element"/>
        /// </summary>
        Element = 6
    }
}
