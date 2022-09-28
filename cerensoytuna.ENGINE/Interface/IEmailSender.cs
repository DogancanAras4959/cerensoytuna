using cerensoytuna.CORE.EmailConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cerensoytuna.ENGINES.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}
