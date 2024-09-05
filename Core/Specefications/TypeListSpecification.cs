using Core.Entites;

namespace Core.Specefications;

public class TypeListSpecification : BaseSpecefication<Product, string>
{

    public TypeListSpecification()
    {
        AddSelect(x => x.Type);
        ApplyDistinct();
    }
}
