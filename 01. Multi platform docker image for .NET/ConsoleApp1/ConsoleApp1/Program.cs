// See https://aka.ms/new-console-template for more information


using LdapForNet;
using LdapForNet.Native;

Console.WriteLine("Hello, World!");

using (var cn = new LdapConnection())
{
    cn.Connect("ldap.stdynamo.dev", 389, Native.LdapSchema.LDAP);
    cn.Bind(LdapForNet.Native.Native.LdapAuthMechanism.SIMPLE,"cn=adminstdyn,dc=stdynamo,dc=dev","vq0Ko|a0XJ0:e+|:TId9");
}

Console.WriteLine("Done.");