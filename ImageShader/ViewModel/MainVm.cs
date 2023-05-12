using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShader.ViewModel
{
    public class MainVm : ViewModelBase
    {
        public ColorKeyAlphaHue _eff = new ColorKeyAlphaHue();
        public ColorKeyAlphaHue eff
        {
            get { return _eff; }
            set { _eff = value; }
        }

        private long _size1 = 0;
        public long size1
        {
            get { return _size1; }
            set { Set("size1", ref _size1, value); }
        }

        private long _size2 = 0;
        public long size2
        {
            get { return _size2; }
            set { Set("size2", ref _size2, value); }
        }

        private string? _Background = "#FFFFFF";
        public string? Background
        {
            get { return _Background; }
            set { Set("Background", ref _Background, value); }
        }

        private ObservableCollection<string> _ResourceNameList = new ObservableCollection<string>();
        public ObservableCollection<string> ResourceNameList
        {
            get { return _ResourceNameList; }
            set { _ResourceNameList = value; }
        }

        private string _ResourceNameItem;
        public string ResourceNameItem
        {
            get { return _ResourceNameItem; }
            set { Set("ResourceNameItem", ref _ResourceNameItem, value); }
        }

        public MainVm()
        {
            ResourceNameList.Add("building.jpg");
            ResourceNameList.Add("city.jpg");
            ResourceNameList.Add("city2.jpg");
            ResourceNameList.Add("countryside.jpg");
            ResourceNameList.Add("flower.jpg");
            ResourceNameList.Add("parrot.jpg");
            ResourceNameList.Add("person.jpg");

        }
    }
}
