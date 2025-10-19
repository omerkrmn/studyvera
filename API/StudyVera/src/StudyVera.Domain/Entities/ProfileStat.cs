using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities;

public class ProfileStat
{
    public int Id { get; set; }
    // buradaki amaç kullanıcıların genel performansını izlemek.
    public int Score { get; set; }

    public Guid UserId { get; set; }
}
