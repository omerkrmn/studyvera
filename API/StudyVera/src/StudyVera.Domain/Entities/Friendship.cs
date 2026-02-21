using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StudyVera.Domain.Entities;

[Index(nameof(RequestorId), nameof(ReceiverId))]
public class Friendship
{
    public int Id { get; set; }

    public Guid RequestorId { get; set; }
    public AppUser Requestor { get; set; }

    public Guid ReceiverId { get; set; }
    public AppUser Receiver { get; set; }

    public FriendshipStatus Status { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ActionDate { get; set; }
}

