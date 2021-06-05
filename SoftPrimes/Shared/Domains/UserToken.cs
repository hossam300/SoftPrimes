using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.Domains
{
    public partial class UserToken
    {
        [Key]
        public int Id { get; set; }
        public string AccessTokenHash { get; set; }
        public DateTimeOffset AccessTokenExpiresDateTime { get; set; }
        [Required]
        [StringLength(450)]
        public string RefreshTokenIdHash { get; set; }
        [StringLength(450)]
        public string RefreshTokenIdHashSource { get; set; }
        public DateTimeOffset RefreshTokenExpiresDateTime { get; set; }
        public string UserId { get; set; }
        public int? ApplicationType { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserTokens")]
        public virtual Agent User { get; set; }

    }
}
