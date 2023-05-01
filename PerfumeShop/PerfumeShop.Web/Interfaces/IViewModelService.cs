namespace PerfumeShop.Web.Interfaces
{
    public interface IViewModelService<TModel, TViewModel>
        where TModel : Entity
        where TViewModel : EntityViewModel
    {
        public Task<IEnumerable<TViewModel>> GetViewModelsAsync();
        public Task<TViewModel> GetViewModelByIdAsync(int id);
        public Task<TModel> CreateModelAsync(TViewModel viewModel);
        public Task<TModel> UpdateModelAsync(TViewModel viewModel);
        public Task<TModel> DeleteModelAsync(TViewModel viewModel);
    }
}
