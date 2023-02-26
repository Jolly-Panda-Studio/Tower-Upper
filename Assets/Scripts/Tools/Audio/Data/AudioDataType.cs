namespace Lindon.Framwork.Audio.Data
{
    public enum AudioDataType
    {
        /// <summary>
        /// Control of playback and pause of these sounds is by functions
        /// </summary>
        Systematically,
        /// <summary>
        /// The sounds of this category are played once and paused for a period of time, and this cycle continues
        /// </summary>
        Randomly,
        /// <summary>
        /// The sounds of this category play for a random period of time and then pause for a random period of time, and this cycle continues.
        /// </summary>
        Timely,
        /// <summary>
        /// The sounds of this category are played randomly and without interruption
        /// </summary>
        Continued,
        /// <summary>
        /// 
        /// </summary>
        CullingRandomly,
    }

    /* +------------------+---------------+----------------+---------+------------------------------------------------------------------------------------------------------------------------------------+ */
    /* |    Data Type     | Has Play Time | Has Pause Time | Is loop |                                                            Description                                                             | */
    /* +------------------+---------------+----------------+---------+------------------------------------------------------------------------------------------------------------------------------------+ */
    /* | Systematically   | false         | false          | Manual  | Control of playback and pause of these sounds is by functions                                                                      | */
    /* | Randomly         | false         | true           | false   | The sounds of this category are played once and paused for a period of time, and this cycle continues                              | */
    /* | Timely           | true          | true           | true    | The sounds of this category play for a random period of time and then pause for a random period of time, and this cycle continues. | */
    /* | Continued        | false         | false          | false   | The sounds of this category are played randomly and without interruption.                                                          | */
    /* | Culling Randomly | false         | true           | false   |                                                                                                                                    | */
    /* +------------------+---------------+----------------+---------+------------------------------------------------------------------------------------------------------------------------------------+ */
}