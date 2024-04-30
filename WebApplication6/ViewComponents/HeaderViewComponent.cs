using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Context;
using WebApplication6.Models;
using WebApplication6.ViewModel;

namespace WebApplication6.ViewComponents;

public class HeaderViewComponent : ViewComponent
{
	private readonly RioDbContext _context;
	private readonly UserManager<AppUser> _userManager;

	public HeaderViewComponent(RioDbContext context)
	{
		_context = context;
	}

	public async Task<IViewComponentResult> InvokeAsync()
	{
		var user = await _userManager.FindByNameAsync(User.Identity.Name);
		var settings = await _context.Settings.ToDictionaryAsync(x => x.Key, x => x.Value);
		var basketItems = await _context.BasketItems.Where(b => b.AppUserId == user.Id).ToListAsync();

		HeaderViewModel headerViewModel = new HeaderViewModel
		{
			Settings = settings,
			BasketItems = basketItems,

		};

		return View(settings);
	}
}