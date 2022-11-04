using ApiViewerBlazor.DTO;

namespace ApiViewerBlazor.ViewModels
{
    public class ShowViewModel
    {
        public bool IsExpanded { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<PersonViewModel> Persons { get; set; }
    }
}
