using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Langulag.EF
{
    public partial class Client
    {
        public string FIO { get => $"{SecondName}{FirstName}{Patronymic}"; }

        public DateTime? LastVisit
        {
            get
            {
                return ClientService.LastOrDefault()?.DateStart;
            }
        }
        public int qtyOfVisits
        {
            get
            {
                return ClientService.Count;
            }
        }
        public List<Tag> Tags
        {
            get
            {
                return Tags.ToList();
            }
        }
        public string ClientGender
        {
            get
            {
                return Gender.Title;
            }
        }
    }
}
