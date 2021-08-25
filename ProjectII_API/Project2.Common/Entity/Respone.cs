using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Common.Entity
{
    public class Respone
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Lỗi gửi DEV
        /// </summary>
        public string DevMsg { get; set; }
        /// <summary>
        /// Lỗi gửi Người dùng
        /// </summary>
        public string UserMsg { get; set; }
    }
}
