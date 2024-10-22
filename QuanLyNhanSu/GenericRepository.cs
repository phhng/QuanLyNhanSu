using QuanLyNhanSu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanSu
{
    public class GenericRepository<T> where T : class
    {
        private Model1 context;

        public GenericRepository()
        {
            context = new Model1();
        }

        // Lấy tất cả thực thể với chọn lọc (nếu có) (ví dụ: lọc nhân viên theo chức vụ)
        public List<T> GetAll(Func<T, bool> filter = null)
        {
            if (filter != null)
            {
                return context.Set<T>().Where(filter).ToList();
            }
            return context.Set<T>().ToList();
        }

        // Tìm thực thể theo ID
        public T FindById<TKey>(TKey id, Func<T, TKey> keySelector)
        {
            return context.Set<T>().FirstOrDefault(e => EqualityComparer<TKey>.Default.Equals(keySelector(e), id));
        }

        public void InsertUpdate(T entity, Func<T, bool> existsFilter)
        {
            var existingEntity = context.Set<T>().FirstOrDefault(existsFilter);

            if (existingEntity != null)
            {
                context.Entry(existingEntity).CurrentValues.SetValues(entity); // cập nhật DB nếu đã tồn tại
            }
            else
            {
                context.Set<T>().Add(entity); // thêm nhiều thực thể
            }
            context.SaveChanges();
        }

        // Xóa thực thể theo thuộc tính
        public void DeleteById<TKey>(TKey id, Func<T, TKey> keySelector)
        {
            var entity = context.Set<T>().FirstOrDefault(e => EqualityComparer<TKey>.Default.Equals(keySelector(e), id));

            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
            else
            {
                throw new Exception($"{typeof(T).Name} not found.");
            }
        }
    }
}
/*
 Một số ví dụ để xài trong bài
Ưu tiên gọi GenericRepository ở đầu class

Kiểu dữ liệu var có thể bỏ ở trong hoặc ở ngoài phương thức nhưng ưu tiên bỏ bên trong phương thức để
k quá tải dữ liệu lúc chạy form

ví dụ:

public class frmNhanVien(){
    var employeeRepository = new GenericRepository<HOSONV>();
    private void LoadForm(){
        var allEmployees = employeeRepository.GetAll();
        ........
        ........ viết code để xuất tất cả thực thể lên form
    }
}
 1. Tìm nhân viên theo ID
    var employeeRepository = new GenericRepository<HOSONV>();
    var employee = employeeRepository.FindById("NV001", e => e.MANV); // Find employee by ID

2. Thêm/sửa thông tin
    var employeeRepository = new GenericRepository<HOSONV>();
    var employee = new HOSONV //đây là ví dụ thêm dữ liệu mẫu, bài mình nên lấy từ input của textbox vv...
    {
        MANV = "NV001",
        Hoten = "John Doe",
        // Set other properties...
    };

    // Thêm nếu k tồn tại, update nếu đã tồn tại
    employeeRepository.InsertUpdate(employee, e => e.MANV == employee.MANV);

3. Xóa theo ID
    var employeeRepository = new GenericRepository<HOSONV>();
    employeeRepository.DeleteById("NV001", e => e.MANV); // Delete employee by ID
 */
