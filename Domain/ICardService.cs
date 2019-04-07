using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
public interface ICardService
{
    Task<List<Card>> GetCards(string keyword);
}