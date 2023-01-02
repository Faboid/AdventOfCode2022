using AdventOfCode2022.Core.Shared;

namespace AdventOfCode2022.Core.Day_7;

public record Directory(string Name, Directory? Parent = null) {

    private readonly Dictionary<string, Directory> _directories = new();
    private readonly Dictionary<string, File> _files = new();

    public Directory GetOrAdd(string name) {
        if(!_directories.TryGetValue(name, out var dir)) {
            dir = new(name, this);
            _directories[name] = dir;
        }
        return dir;
    }


    public void NestFile(File file) {
        if(!_directories.ContainsKey(file.Name)) {
            _files[file.Name] = file;
        }
    }

    public void NestDirectory(Directory directory) {
        if(!_directories.ContainsKey(directory.Name)) {
            _directories[directory.Name] = directory;
        }
    }

    /// <summary>
    /// Returns the size of the files held within the directory. Ignores children directories.
    /// </summary>
    /// <returns></returns>
    public int GetLimitedSize() => _files.Values.Sum(x => x.Size);
    public int GetSize() => _directories.Values.Sum(x => x.GetSize()) + _files.Values.Sum(x => x.Size);
    public IEnumerable<File> EnumerateFiles() => _files.Values;
    public IEnumerable<Directory> EnumerateDirectories() => _directories.Values.Select(x => x.EnumerateDirectoriesWithThis()).SelectMany(x => x);
    private IEnumerable<Directory> EnumerateDirectoriesWithThis() => _directories.Values.Select(x => x.EnumerateDirectoriesWithThis()).SelectMany(x => x).Concat(this);

}
