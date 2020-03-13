using EmployeeData;
using EmployeeData.Model;
using EmployeeData.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace EmployeeData.ViewModel
{
    class ViewModelMain: INotifyPropertyChanged
    {

        public ViewModelMain()
        {
            Employees = new ObservableCollection<Emloyee>();
            EmployeesV = new List<Emloyee>();
            Departments = new ObservableCollection<DistinctDepartment>();
        }

        //commands

        private bool _isOpen;
        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen == value) return;
                _isOpen = value;
                OnPropertyChanged("IsOpen");
            }
        }

        private Relay_Command _popuphandle;
        public Relay_Command Popuphandle
        {
            get
            {
                return _popuphandle ??
                  (_popuphandle = new Relay_Command(obj=> OpenHandle()));
            }
        }

        private Relay_Command _download;
        public Relay_Command Download
        {
            get
            {
                return _download ??
                  (_download = new Relay_Command(obj => OpenFileDialog()));
            }
        }

        private Relay_Command _shutdown;
        public Relay_Command Shutdown
        {
            get
            {
                return _shutdown ??
                  (_shutdown = new Relay_Command(obj => { Application.Current.Shutdown(); }));
            }
        }

        private Relay_Command _change1;
        public Relay_Command Change1
        {
            get
            {
                return _change1 ??
                  (_change1 = new Relay_Command(obj => ShowChange()));
            }
        }

        //DataContext of all data
        public ObservableCollection<Emloyee> Employees { get; set; }


        //Что выводим в таблицу
        public List<Emloyee> _showv;
        public List<Emloyee> EmployeesV {
            get { return _showv; }
            set
            {
                _showv = value;
                OnPropertyChanged("EmployeesV");
            }
        }

        //Список всех отделов
        public ObservableCollection<DistinctDepartment> Departments { get; set; }
            
           

        
        //Добавление данных
        public async Task OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var strr in openFileDialog.FileNames)
                {
                    try
                    {
                        AddEmployee(strr);
                    }
                    catch { 
                        MessageBox.Show("Файл некорректный, выберите другой"); 
                    }
                }
            }
        }

        //Если данные изменились, то изменим и представление
        private void Departments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ShowChange().Wait();
        }


        public async Task OpenHandle()
        {
            if (IsOpen) { IsOpen = false; } else { IsOpen = true; }
            
        }

        //Изменение представления в соотвествии с критериями отбора
        public async Task ShowChange()
        {
            var selected = Departments.Where(e => e.Show).Select(e=>e.DepartmentName);
      
            EmployeesV = Employees.Where(e => selected.Contains(e.Department)).OrderBy(e=>e.Department).ToList();
        }


        

        //Добавление из конкретного файла
        public async Task AddEmployee(string FilePath)
        {
            var xml = XDocument.Load(FilePath);
            
            foreach (var item in xml.Descendants("User").Select(x => new Emloyee { Name = x.Attribute("Name").Value,
                Department= (string)x.Attribute("Department").Value,
                Gender= (string)x.Attribute("Gender").Value,
            }))
            {
                
                Employees.Insert(0, item);
                DistinctDepartment temp = new DistinctDepartment { DepartmentName = item.Department, Show = true };
                if(!Departments.Select(x=>x.DepartmentName).ToArray().Contains(temp.DepartmentName))
                {
                    Departments.Insert(0, temp);
                }

            }
            
            EmployeesV = Employees.ToList();
        }


        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
