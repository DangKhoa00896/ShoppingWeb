using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingWeb.Data;
using ShoppingWeb.Models;

namespace ShoppingWeb.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ShoppingDbContext _context;
        public SanPhamController(ShoppingDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var list = await _context.SanPhams.Include(s => s.DanhMuc).ToListAsync();
            return View(list);
        }

        //----->>>> Thêm các chức năng CRUD

        // Nạp danh sách danh mục cho dropdown
        private void LoadDanhMuc(object? selected = null)
        {
            ViewBag.DanhMucMa = new SelectList(
                _context.DanhMucs.OrderBy(d => d.Ten), "Ma", "Ten", selected);
        }

        // GET: /SanPham/Details/SP01
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null) return NotFound();

            var sanPham = await _context.SanPhams
                .Include(s => s.DanhMuc)
                .FirstOrDefaultAsync(s => s.Ma == id);
            if (sanPham == null) return NotFound();

            return View(sanPham);
        }

        // GET: /SanPham/Create
        public IActionResult Create()
        {
            LoadDanhMuc();
            return View();
        }

        // POST: /SanPham/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ma,Ten,Gia,DanhMucMa")] SanPham sanPham)
        {
            if (await _context.SanPhams.AnyAsync(s => s.Ma == sanPham.Ma))
                ModelState.AddModelError(nameof(SanPham.Ma), "Mã sản phẩm đã tồn tại.");

            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDanhMuc(sanPham.DanhMucMa);
            return View(sanPham);
        }

        // GET: /SanPham/Edit/SP01
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null) return NotFound();

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null) return NotFound();

            LoadDanhMuc(sanPham.DanhMucMa);
            return View(sanPham);
        }

        // POST: /SanPham/Edit/SP01
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Ma,Ten,Gia,DanhMucMa")] SanPham sanPham)
        {
            if (id != sanPham.Ma) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.SanPhams.AnyAsync(s => s.Ma == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            LoadDanhMuc(sanPham.DanhMucMa);
            return View(sanPham);
        }

        // GET: /SanPham/Delete/SP01
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null) return NotFound();

            var sanPham = await _context.SanPhams
                .Include(s => s.DanhMuc)
                .FirstOrDefaultAsync(s => s.Ma == id);
            if (sanPham == null) return NotFound();

            return View(sanPham);
        }

        // POST: /SanPham/Delete/SP01
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        //----->>>>
    }
}