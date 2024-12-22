using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserOperationClaimRepository.Constans
{
    public class UserOperationClaimMessages
    {
        public static string Added = "Yetki ataması başarıyla oluşturuldu";
        public static string Update = "Yetki ataması başarıyla güncellendi";
        public static string Deleted = "Yetki ataması başarıyla silindi";
        public static string OperationClaimSetExist = "Bu kullanıcıya yetki atanmış";
        public static string OperationClaimNotExist = "Seçtiğiniz yetki bilgisi yetkilerde yok";
        public static string UserNotExist = "Seçtiğiniz kullanıcı yok";
    }
}
