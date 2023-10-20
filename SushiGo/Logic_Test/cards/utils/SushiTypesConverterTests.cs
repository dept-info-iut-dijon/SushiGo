using Logic_Layer.cards;
using Logic_Layer.cards.utils;
using Logic_Layer.logic_exceptions;

namespace LogicTest.cards.utils;

public class SushiTypesConverterTests
{
    private readonly SushiTypesConverter converter = new SushiTypesConverter();
    
    [Fact]
    public void StringToSushi_ConvertCorrectly()
    {
        Assert.Equal(SushiTypes.CALAMARI, converter.StringToSushi("calamar"));
        Assert.Equal(SushiTypes.OMELETTE, converter.StringToSushi("omelette"));
        Assert.Equal(SushiTypes.SALMON, converter.StringToSushi("saumon"));
    }

    [Fact]
    public void StringToSushi_ThrowExceptionWhenInvalidParameter()
    {
        Assert.Throws<SushiTypeDoesNotExist>(() => converter.StringToSushi("ce paramètre n'est pas valide"));
    }

    [Fact]
    public void SushiToString_ConvertCorrectly()
    {
        Assert.Equal("Calamar", converter.SushiToString(SushiTypes.CALAMARI));
        Assert.Equal("Omelette", converter.SushiToString(SushiTypes.OMELETTE));
        Assert.Equal("Saumon", converter.SushiToString(SushiTypes.SALMON));
    }
}