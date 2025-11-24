using System;
using System.Collections.Generic;

//
// Общий компонент файловой системы (и файл, и папка)
//
interface IFileSystemComponent
{
    string Name { get; }
    long GetSize(); // размер в байтах (или условных единицах)
}

//
// Лист — Файл
//
class File : IFileSystemComponent
{
    public string Name { get; }
    public long Size { get; }

    public File(string name, long size)
    {
        Name = name;
        Size = size;
    }

    public long GetSize()
    {
        return Size;
    }
}

//
// Компоновщик — Папка
//
class Directory : IFileSystemComponent
{
    public string Name { get; }
    
    // Папка может содержать и файлы, и другие папки
    private readonly List<IFileSystemComponent> _children = new List<IFileSystemComponent>();

    public Directory(string name)
    {
        Name = name;
    }

    public void Add(IFileSystemComponent component)
    {
        _children.Add(component);
    }

    public void Remove(IFileSystemComponent component)
    {
        _children.Remove(component);
    }

    public long GetSize()
    {
        long totalSize = 0;
        foreach (var child in _children)
        {
            totalSize += child.GetSize();
        }
        return totalSize;
    }
}

//
// Пример использования
//
class Program
{
    static void Main(string[] args)
    {
        // Файлы
        var file1 = new File("file1.txt", 100);
        var file2 = new File("file2.jpg", 2000);
        var file3 = new File("file3.docx", 500);

        // Папки
        var root = new Directory("root");
        var images = new Directory("images");
        var docs = new Directory("docs");

        // Строим дерево
        root.Add(file1);      // file1 лежит в корне
        root.Add(images);     // папка images в корне
        root.Add(docs);       // папка docs в корне

        images.Add(file2);    // картинка в images
        docs.Add(file3);      // документ в docs

        Console.WriteLine($"Размер file1: {file1.GetSize()}");
        Console.WriteLine($"Размер images: {images.GetSize()}");
        Console.WriteLine($"Размер docs: {docs.GetSize()}");
        Console.WriteLine($"Размер root: {root.GetSize()}");
    }
}




