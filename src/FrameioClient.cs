using System;

namespace FrameioNet
{
    public class FrameioClient
    {
        #region Constructors

        /// <summary>
        /// Initialize client with a developer token
        /// <param name="developerToken">Developer token</param>
        /// </summary>
        public FrameioClient(string developerToken) {
            SetDeveloperToken(developerToken);
        }

        #endregion Constructors

        #region Declarations

        private static string _developerToken;

        #endregion Declarations 

        #region Private Methods

        /// <summary>
        /// Set the developer token
        /// <param name="developerToken">Developer token</param>
        /// </summary>
        private static void SetDeveloperToken(string developerToken) {
            if (string.IsNullOrWhiteSpace(developerToken)) {
                throw new ArgumentException("Developer token cannot be empty or null");
            }

            _developerToken = developerToken;
        }

        #endregion Private Methods
    }
}
