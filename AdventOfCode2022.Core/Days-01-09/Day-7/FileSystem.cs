namespace AdventOfCode2022.Core.Day_7;

public class FileSystem {

    public FileSystem() {
        _pointer = _root;
    }

    private readonly Directory _root = new("/");
    private Directory _pointer;

    public Directory GetRoot() => _root;

    public void ChangeDirectory(string name) {

        if(name == "/") {
            _pointer = _root;
            return;
        }

        if(name == "..") {
            _pointer = _pointer.Parent ?? _root;
            return;
        }

        _pointer = _pointer.GetOrAdd(name);

    }

    public void AddDirectory(string dir) {
        _pointer.NestDirectory(new Directory(dir, _pointer));
    }

    public void AddFile(string name, int size) {
        _pointer.NestFile(new File(name, size));
    }

}
