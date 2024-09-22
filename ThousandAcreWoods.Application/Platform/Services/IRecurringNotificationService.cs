using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Application.Platform.Services;
public interface IRecurringNotificationService
{
    event EventHandler OnVeryOften;
    event EventHandler OnOften;
    event EventHandler OnNormal;
    event EventHandler OnInfrequent;
    event EventHandler OnVeryInfrequent;
    event EventHandler OnDaily;

    public void Fire();

}
