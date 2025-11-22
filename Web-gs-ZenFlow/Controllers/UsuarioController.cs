using Microsoft.AspNetCore.Mvc;
using Web_gs_ZenFlow.Application.DTOs.Usuario;
using Web_gs_ZenFlow.Application.UseCase;

namespace Web_gs_ZenFlow.Controllers;

public class UsuarioController : Controller
{
    private readonly IUsuarioUseCase _usuarioUseCase;

    public UsuarioController(IUsuarioUseCase usuarioUseCase)
    {
        _usuarioUseCase = usuarioUseCase;
    }

    // GET: Usuario
    public async Task<IActionResult> Index()
    {
        var usuarios = await _usuarioUseCase.GetAllUsuariosAsync();
        return View(usuarios);
    }

    // GET: Usuario/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(id);
        
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // GET: Usuario/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Usuario/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUsuarioDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);
        
        try
        {
            await _usuarioUseCase.CreateUsuarioAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(dto);
        }
    }

    // GET: Usuario/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: Usuario/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);
        
        var usuario = await _usuarioUseCase.LoginAsync(dto);
        
        if (usuario == null)
        {
            ModelState.AddModelError("", "Email ou senha incorretos");
            return View(dto);
        }

        // Aqui você pode implementar autenticação/sessão se necessário
        return RedirectToAction(nameof(Details), new { id = usuario.Id });
    }

    // GET: Usuario/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(id);
        
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // GET: Usuario/AlterarEmail/5
    public async Task<IActionResult> AlterarEmail(int id)
    {
        var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(id);
        
        if (usuario == null)
        {
            return NotFound();
        }

        var dto = new AlterarEmailDto { Email = usuario.Email };
        ViewBag.UsuarioId = id;
        return View(dto);
    }

    // POST: Usuario/AlterarEmail/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AlterarEmail(int id, AlterarEmailDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.UsuarioId = id;
            return View(dto);
        }
        
        try
        {
            await _usuarioUseCase.AlterarEmailUsuarioAsync(id, dto);
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.UsuarioId = id;
            return View(dto);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.UsuarioId = id;
            return View(dto);
        }
    }

    // GET: Usuario/AlterarSenha/5
    public async Task<IActionResult> AlterarSenha(int id)
    {
        var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(id);
        
        if (usuario == null)
        {
            return NotFound();
        }

        ViewBag.UsuarioId = id;
        return View();
    }

    // POST: Usuario/AlterarSenha/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AlterarSenha(int id, AlterarSenhaDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.UsuarioId = id;
            return View(dto);
        }
        
        try
        {
            await _usuarioUseCase.AlterarSenhaUsuarioAsync(id, dto);
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.UsuarioId = id;
            return View(dto);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.UsuarioId = id;
            return View(dto);
        }
    }

    // GET: Usuario/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(id);
        
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // POST: Usuario/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _usuarioUseCase.DeleteUsuarioAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound();
            return View(usuario);
        }
    }
}

