using Microsoft.AspNetCore.Mvc;
using Web_gs_ZenFlow.Application.DTOs.Registro;
using Web_gs_ZenFlow.Application.UseCase;

namespace Web_gs_ZenFlow.Controllers;

public class RegistroController : Controller
{
    private readonly IRegistroUseCase _registroUseCase;
    private readonly IUsuarioUseCase _usuarioUseCase;

    public RegistroController(IRegistroUseCase registroUseCase, IUsuarioUseCase usuarioUseCase)
    {
        _registroUseCase = registroUseCase;
        _usuarioUseCase = usuarioUseCase;
    }

    // GET: Registro
    public async Task<IActionResult> Index()
    {
        var registros = await _registroUseCase.GetAllRegistrosAsync();
        return View(registros);
    }

    // GET: Registro/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var registro = await _registroUseCase.GetRegistroByIdAsync(id);
        
        if (registro == null)
        {
            return NotFound();
        }

        return View(registro);
    }

    // GET: Registro/Create
    public async Task<IActionResult> Create(int? usuarioId)
    {
        if (usuarioId.HasValue)
        {
            var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(usuarioId.Value);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewBag.UsuarioId = usuarioId.Value;
            ViewBag.UsuarioNome = usuario.NomeCompleto;
        }
        else
        {
            var usuarios = await _usuarioUseCase.GetAllUsuariosAsync();
            ViewBag.Usuarios = usuarios;
        }
        
        return View();
    }

    // POST: Registro/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateRegistroDto dto, int? usuarioId)
    {
        // Tenta obter usuarioId do form se não foi passado como parâmetro
        if (!usuarioId.HasValue)
        {
            var usuarioIdStr = Request.Form["usuarioId"].ToString();
            if (!string.IsNullOrEmpty(usuarioIdStr) && int.TryParse(usuarioIdStr, out int parsedId))
            {
                usuarioId = parsedId;
            }
        }

        if (!usuarioId.HasValue)
        {
            ModelState.AddModelError("", "Usuário é obrigatório");
            var usuarios = await _usuarioUseCase.GetAllUsuariosAsync();
            ViewBag.Usuarios = usuarios;
            return View(dto);
        }

        if (!ModelState.IsValid)
        {
            var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(usuarioId.Value);
            if (usuario != null)
            {
                ViewBag.UsuarioId = usuarioId.Value;
                ViewBag.UsuarioNome = usuario.NomeCompleto;
            }
            else
            {
                var usuarios = await _usuarioUseCase.GetAllUsuariosAsync();
                ViewBag.Usuarios = usuarios;
            }
            return View(dto);
        }
        
        try
        {
            var registro = await _registroUseCase.CreateRegistroAsync(usuarioId.Value, dto);
            return RedirectToAction(nameof(Details), new { id = registro.Id });
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(usuarioId.Value);
            if (usuario != null)
            {
                ViewBag.UsuarioId = usuarioId.Value;
                ViewBag.UsuarioNome = usuario.NomeCompleto;
            }
            else
            {
                var usuarios = await _usuarioUseCase.GetAllUsuariosAsync();
                ViewBag.Usuarios = usuarios;
            }
            return View(dto);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("", ex.Message);
            var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(usuarioId.Value);
            if (usuario != null)
            {
                ViewBag.UsuarioId = usuarioId.Value;
                ViewBag.UsuarioNome = usuario.NomeCompleto;
            }
            else
            {
                var usuarios = await _usuarioUseCase.GetAllUsuariosAsync();
                ViewBag.Usuarios = usuarios;
            }
            return View(dto);
        }
    }

    // GET: Registro/ByUsuario/5
    public async Task<IActionResult> ByUsuario(int usuarioId)
    {
        try
        {
            var registros = await _registroUseCase.GetRegistrosByUsuarioIdAsync(usuarioId);
            var usuario = await _usuarioUseCase.GetUsuarioByIdAsync(usuarioId);
            
            ViewBag.Usuario = usuario;
            return View(registros);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound();
        }
    }

    // GET: Registro/Delete/5
    public async Task<IActionResult> Delete(int id, int? usuarioId)
    {
        var registro = await _registroUseCase.GetRegistroByIdAsync(id);
        
        if (registro == null)
        {
            return NotFound();
        }

        ViewBag.UsuarioId = usuarioId ?? registro.UsuarioId;
        return View(registro);
    }

    // POST: Registro/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, int usuarioId)
    {
        try
        {
            await _registroUseCase.DeleteRegistroAsync(id, usuarioId);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            var registro = await _registroUseCase.GetRegistroByIdAsync(id);
            if (registro == null)
                return NotFound();
            ViewBag.UsuarioId = usuarioId;
            return View(registro);
        }
        catch (UnauthorizedAccessException ex)
        {
            ModelState.AddModelError("", ex.Message);
            var registro = await _registroUseCase.GetRegistroByIdAsync(id);
            if (registro == null)
                return NotFound();
            ViewBag.UsuarioId = usuarioId;
            return View(registro);
        }
    }
}

