using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class AVLTree
    {
        private AVLNode root;

        public void Insert(int key)
        {
            root = Insert(root, key);
        }

        public void Delete(int key)
        {
            root = Delete(root, key);
        }

        public bool Find(int key)
        {
            return Find(root, key);
        }

        public void Print()
        {
            Print(root, 0);
        }

        private AVLNode Insert(AVLNode node, int key)
        {
            // Вставка элемента в дерево
            if (node == null)
            {
                return new AVLNode(key);
            }

            if (key < node.Key)
            {
                node.Left = Insert(node.Left, key);
            }
            else if (key > node.Key)
            {
                node.Right = Insert(node.Right, key);
            }
            else // Дубликаты не допускаются
            {
                return node;
            }

            // Обновление высоты вершины
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            // Балансировка дерева
            int balance = GetBalance(node);

            if (balance > 1)
            {
                if (key < node.Left.Key)
                    return RotateRight(node);

                if (key > node.Left.Key)
                {
                    node.Left = RotateLeft(node.Left);
                    return RotateRight(node);
                }
            }

            if (balance < -1)
            {
                if (key > node.Right.Key)
                    return RotateLeft(node);

                if (key < node.Right.Key)
                {
                    node.Right = RotateRight(node.Right);
                    return RotateLeft(node);
                }
            }

            return node;
        }

        private AVLNode Delete(AVLNode node, int key)
        {
            // Удаление элемента из дерева
            if (node == null)
                return node;

            if (key < node.Key)
                node.Left = Delete(node.Left, key);
            else if (key > node.Key)
                node.Right = Delete(node.Right, key);
            else
            {
                if ((node.Left == null) || (node.Right == null))
                    node = (node.Left ?? node.Right);
                else
                {
                    AVLNode temp = MinValueNode(node.Right);
                    node.Key = temp.Key;
                    node.Right = Delete(node.Right, temp.Key);
                }
            }

            if (node == null)
                return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
            int balance = GetBalance(node);

            if (balance > 1)
            {
                if (GetBalance(node.Left) >= 0)
                    return RotateRight(node);

                if (GetBalance(node.Left) < 0)
                {
                    node.Left = RotateLeft(node.Left);
                    return RotateRight(node);
                }
            }

            if (balance < -1)
            {
                if (GetBalance(node.Right) <= 0)
                    return RotateLeft(node);

                if (GetBalance(node.Right) > 0)
                {
                    node.Right = RotateRight(node.Right);
                    return RotateLeft(node);
                }
            }

            return node;
        }

        private bool Find(AVLNode node, int key)
        {
            if (node == null)
                return false;

            if (key == node.Key)
                return true;

            if (key < node.Key)
                return Find(node.Left, key);

            return Find(node.Right, key);
        }

        private void Print(AVLNode node, int level)
        {
            if (node == null)
                return;

            Print(node.Right, level + 1);

            Console.WriteLine(new string(' ', 4 * level) + node.Key);

            Print(node.Left, level + 1);
        }

        private int Height(AVLNode node)
        {
            return (node != null) ? node.Height : 0;
        }

        private int GetBalance(AVLNode node)
        {
            return (node != null) ? Height(node.Left) - Height(node.Right) : 0;
        }

        private AVLNode RotateRight(AVLNode y)
        {
            AVLNode x = y.Left;
            AVLNode T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        private AVLNode RotateLeft(AVLNode x)
        {
            AVLNode y = x.Right;
            AVLNode T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        private AVLNode MinValueNode(AVLNode node)
        {
            AVLNode current = node;

            while (current.Left != null)
            {
                current = current.Left;
            }

            return current;
        }

        // первая перегрузка дерево + число
        public static AVLTree operator +(AVLTree tree, int value)
        {
            if (tree == null)
                return null;

            AVLTree result = new AVLTree();
            CloneAndIncreaseKeys(tree.root, result, value);

            return result;
        }

        private static void CloneAndIncreaseKeys(AVLNode source, AVLTree resultTree, int value)
        {
            if (source == null)
                return;

            int newKey = source.Key + value;
            resultTree.Insert(newKey);

            CloneAndIncreaseKeys(source.Left, resultTree, value);
            CloneAndIncreaseKeys(source.Right, resultTree, value);
        }

        public static AVLTree operator +(AVLTree tree1, AVLTree tree2)
        {
            if (tree1 == null || tree2 == null)
                return null;

            AVLTree result = new AVLTree();

            // Добавляем все ключи из первого дерева
            CloneAndInsertKeys(tree1.root, result);

            // Добавляем все ключи из второго дерева
            CloneAndInsertKeys(tree2.root, result);

            return result;
        }

        private static void CloneAndInsertKeys(AVLNode source, AVLTree resultTree)
        {
            if (source == null)
                return;

            // Вставляем ключ из текущего узла в результат
            resultTree.Insert(source.Key);

            // Рекурсивно обрабатываем левое и правое поддерево
            CloneAndInsertKeys(source.Left, resultTree);
            CloneAndInsertKeys(source.Right, resultTree);
        }
    }
}