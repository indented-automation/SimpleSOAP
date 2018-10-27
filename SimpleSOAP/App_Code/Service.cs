using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.Services;
using Newtonsoft.Json;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{
    List<Atom> atoms;

    public Service () {
        using (StreamReader r = new StreamReader(Server.MapPath("atoms.json")))
        {
            string json = r.ReadToEnd();
            this.atoms = JsonConvert.DeserializeObject<List<Atom>>(json);
        }
    }

    [WebMethod]
    public Atom[] GetAtoms()
    {
        return this.atoms.ToArray();
    }

    [WebMethod]
    public Atom GetAtom(string Name)
    {
        return this.FindAtom(Name);
    }
    
    [WebMethod]
    public int GetAtomicNumber(string Name)
    {
        return this.FindAtom(Name).AtomicNumber;
    }

    [WebMethod]
    public string GetAtomicMass(string Name)
    {
        return this.FindAtom(Name).AtomicMass;
    }

    [WebMethod]
    public string GetElementSymbol(string Name)
    {
        return this.FindAtom(Name).Symbol;
    }

    private Atom FindAtom(string Name)
    {
        return this.atoms.Where(atom => atom.Name == Name || atom.Symbol == Name).First<Atom>();
    }
}