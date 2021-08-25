using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Common.Entity
{
    /// <summary>
    /// Tạo class User,
    /// Thông tin User
    /// </summary>
    public class User
    {
        #region Property Người dùng
        /// <summary>
        /// Khóa chính
        /// </summary>
        /// Id người dùng
        [Column] public Guid UserId { get; set; }
        /// <summary>
        /// Họ và tên
        /// </summary>
        [Column] public string FullName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        [Column] public string Gender { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        [Column] public string Address { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Column] public string PhoneNumber { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Column] public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Tài khoản
        /// </summary>
        [Column] public string Account { get; set; }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Column] public string Password { get; set; }
        /// <summary>
        /// Trạng thái
        /// </summary>
        public string Authority_Name { get; set; }
        #endregion
    }
}
