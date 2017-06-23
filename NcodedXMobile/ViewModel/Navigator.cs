using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NcodedXMobile.ViewModel
{
    /// <summary>
    /// Manipulates the given navigation stack. Will create and bind subclassed <see cref="PageViewModel"/> to each created Page.
    /// </summary>
    public class Navigator
    {
        private INavigation _navigation;
        /// <summary>
        /// Initzialize a new instance of the <see cref="Navigator"/>-class.
        /// </summary>
        /// <param name="navigation">The interface used for navigation.</param>
        public Navigator(INavigation navigation)
        {
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
        }
        /// <summary>
        /// Pushes a created <see cref="Page"/> with bound <see cref="PageViewModel"/> to the navigation stack. 
        /// </summary>
        /// <typeparam name="TPage">Page to create and push.</typeparam>
        /// <typeparam name="TViewModel">ViewModel for the created Page.</typeparam>
        /// <param name="args">Arguments for the created ViewModel.</param>
        /// <returns>Awaitable Task.</returns>
        public async Task Push<TPage, TViewModel>(params object[] args)
            where TPage : Page
            where TViewModel : PageViewModel
        {
            var page = PageFactory<TPage, TViewModel>(this, args);

            await _navigation.PushAsync(page);
        }
        /// <summary>
        /// Pushes a created <see cref="Page"/> with bound <see cref="PageViewModel"/> to the navigation stack modally. 
        /// </summary>
        /// <typeparam name="TPage">Page to create and push.</typeparam>
        /// <typeparam name="TViewModel">ViewModel for the created Page.</typeparam>
        /// <param name="args">Arguments for the created ViewModel.</param>
        /// <returns>Awaitable Task.</returns>
        public async Task PushModal<TPage, TViewModel>(params object[] args)
            where TPage : Page
            where TViewModel : PageViewModel
        {
            var page = PageFactory<TPage, TViewModel>(this, args);

            await _navigation.PushModalAsync(page);
        }
        /// <summary>
        /// Removes the <see cref="Page"/> from navigation stack.
        /// </summary>
        /// <param name="page"><see cref="Page"/> to remove.</param>
        public void Remove(Page page) => _navigation.RemovePage(page);
        /// <summary>
        /// Insert a <see cref="Page"/> before another.
        /// </summary>
        /// <param name="page"><see cref="Page"/> to insert.</param>
        /// <param name="before">The <see cref="Page"/> before which we will insert.</param>
        public void InsertBefore(Page page, Page before) => _navigation.InsertPageBefore(page, before);
        /// <summary>
        /// Pops the last page from Navigationstack and returns the Bound VM.
        /// </summary>
        public async Task<PageViewModel> Pop()
        {
            var pageVM = (await _navigation.PopAsync()).BindingContext as PageViewModel;

            return pageVM;
        }
        /// <summary>
        /// Creates a Page and ViewModel and bind them.
        /// </summary>
        /// <typeparam name="TPage">Page to create.</typeparam>
        /// <typeparam name="TViewModel">ViewModel to create.</typeparam>
        /// <param name="args">Arguments for the created ViewModel.</param>
        /// <returns>Created Page. The Created ViewModel is bound to Pages BindingContext.</returns>
        public TPage CreatePage<TPage, TViewModel>(params object[] args)
            where TPage : Page
            where TViewModel : PageViewModel
        {
            return PageFactory<TPage, TViewModel>(this, args);
        }

        private static TPage PageFactory<TPage, TViewModel>(Navigator nav, object[] args)
            where TPage : Page
            where TViewModel : PageViewModel
        {
            // Construct Page and Model
            TPage page = Activator.CreateInstance<TPage>();
            var pageViewModel = Activator.CreateInstance(typeof(TViewModel), args) as PageViewModel;

            if (pageViewModel == null)
                throw new InvalidOperationException($"{typeof(TViewModel).ToString()} have to derive from {nameof(PageViewModel)} in order to work properly.");

            // Register / Inject dependency
            pageViewModel.Register(page, nav);

            // Setup Bindings.
            page.SetBinding(Page.TitleProperty, nameof(PageViewModel.Title));

            // Bind ViewModel to Page.
            page.BindingContext = pageViewModel;
            return page;
        }
    }
}
