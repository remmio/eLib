using System.IO;
using System.Media;
using System.Threading.Tasks;

namespace CLib.Media
{
    /// <summary>
    /// 
    /// </summary>
    public class SoundsHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public static void PlaySoundAsync(Stream stream)
        {
            Task.Run(() =>
            {
                using (stream)
                using (var soundPlayer = new SoundPlayer(stream))
                {
                    soundPlayer.PlaySync();
                }
            });
        }
    }
}
