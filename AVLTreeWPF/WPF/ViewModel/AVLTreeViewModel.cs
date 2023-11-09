using Library;
using AVLTreeWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


public class AVLTreeViewModel : ViewModelBase
{
    private AVLTree avlTree;
    private int inputValue;
    private bool isFound;
    private AVLNode rootNode;

    public AVLTreeViewModel()
    {
        avlTree = new AVLTree();
        rootNode = null;
    }

    public int InputValue
    {
        get { return inputValue; }
        set
        {
            if (inputValue != value)
            {
                inputValue = value;
                OnPropertyChanged("InputValue");
            }

        }
    }

    public bool IsFound
    {
        get { return isFound; }
        set
        {
            isFound = value;
            OnPropertyChanged("IsFound");
        }
    }

    public ObservableCollection<AVLNode> TreeNodes
    {
        get { return new ObservableCollection<AVLNode> { rootNode }; }
    }

    public ICommand AddCommand => new RelayCommand(AddValue);
    public ICommand DeleteCommand => new RelayCommand(DeleteValue);
    public ICommand SaveToFileCommand => new RelayCommand(SaveToFile);

    private void AddValue()
    {
        avlTree.Insert(InputValue);
        UpdateTreeView();
    }

    private void DeleteValue()
    {
        avlTree.Delete(InputValue);
        UpdateTreeView();
    }

    private void UpdateTreeView()
    {
        rootNode = avlTree.Root; // Обновляем корневой узел для отображения в TreeView
        OnPropertyChanged("TreeNodes");
    }

    private void SaveToFile()
    {
        // Здесь реализуйте сохранение дерева в файл
    }
}