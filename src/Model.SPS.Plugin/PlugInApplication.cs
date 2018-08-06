namespace Platform.Model
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace SPS
    {
        /// <summary>
        /// Implements the IPlugInApplication interface.
        /// </summary>
        /// <typeparam name="TApp">Type of application interface</typeparam>
        internal class PlugInApplication<TApp> : IPlugInApplication<TApp>
        {
            /// <summary>
            /// Proxy object to use application by plugin over application interface.
            /// </summary>
            public TApp ApplicationProxy { get; set; }

            /// <summary>
            /// Name of the application.
            /// </summary>
            public string Name { get; set; }
        }
    }

#if PORTABLE
    }

#endif
}