using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD.WPF.Client.APIClient
{
    public interface IClientApplicationConfiguration
    {
        string ServerAddress { get; }
        string AppUri { get; }
        string ClientId { get; }
    }
}
