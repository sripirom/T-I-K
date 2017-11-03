using System;
using System.IO;
using Akka.Configuration;

namespace TIK.ProcessService.Membership.ActorSystems
{
    /// <summary>
    ///     Used to load HOCON definitions from a dedicated HOCON file
    /// </summary>
    public static class HoconLoader
    {
        /// <summary>
        ///     Parses a HOCON <see cref="Config" /> object from an underlying file
        /// </summary>
        /// <param name="path">The path to the HOCON file.</param>
        /// <returns>A parsed <see cref="Config" /> object.</returns>
        public static Config FromFile(string path)
        {
            try
            {
                return ConfigurationFactory.ParseString(File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
    }
}
