// Kelas abstrak Robot sebagai dasar untuk semua jenis robot
public abstract class Robot
{
    public string Nama;
    public int Energi;
    public int Armor;
    public int Serangan;

    // Constructor untuk inisialisasi robot
    protected Robot(string nama, int energi, int armor, int serangan)
    {
        Nama = nama;
        Energi = energi;
        Armor = armor;
        Serangan = serangan;
    }

    // Method virtual untuk menyerang robot lain
    public virtual void Serang(Robot target)
    {
        // Hitung damage yang diberikan
        int serangan_yangdiberikan = Serangan;

        // Cek apakah target masih punya armor
        if (target.Armor > 0)
        {
            if (serangan_yangdiberikan >= target.Armor)
            {
                // Jika serangan lebih besar dari armor, hancurkan armor
                serangan_yangdiberikan -= target.Armor;
                target.Armor = 0;
                Console.WriteLine($"{Nama} menghancurkan armor {target.Nama}.");
            }
            else
            {
                // Jika tidak, kurangi armor
                target.Armor -= serangan_yangdiberikan;
                serangan_yangdiberikan = 0;
                Console.WriteLine($"{Nama} mengurangi armor {target.Nama} sebanyak {Serangan}.");
            }
        }

        // Jika masih ada sisa serangan, kurangi energi target
        if (serangan_yangdiberikan > 0)
        {
            target.Energi -= serangan_yangdiberikan;
            if (target.Energi < 0)
            {
                target.Energi = 0;
            }
            Console.WriteLine($"{Nama} menyerang {target.Nama}, mengurangi energi sebanyak {serangan_yangdiberikan}.");
        }

        // Cek apakah target sudah mati
        if (target.Energi <= 0)
        {
            Console.WriteLine($"Robot {target.Nama} telah mati.");
        }
    }

    // Method abstrak untuk menggunakan kemampuan
    public abstract void GunakanKemampuan(IKemampuan kemampuan, Robot target);

    // Method untuk mencetak informasi robot
    public void CetakInformasi()
    {
        Console.WriteLine($"Nama Robot\t: {Nama}\nEnergi\t\t: {Energi}\nArmor\t\t: {Armor}\nSerangan\t: {Serangan}\n");
    }
}

// Interface untuk semua jenis kemampuan
public interface IKemampuan
{
    void Gunakan(Robot pengguna, Robot target);
    void KurangiCooldown();
    bool KeteranganCooldown();
}

// Implementasi kemampuan Perbaikan
public class Perbaikan : IKemampuan
{
    int cooldown;

    public void Gunakan(Robot pengguna, Robot target)
    {
        if (KeteranganCooldown())
        {
            Console.WriteLine("Kemampuan perbaikan sedang Cooldown!");
            return;
        }

        // Lakukan perbaikan
        Console.WriteLine("Robot sedang melakukan perbaikan.");
        pengguna.Energi += 20;
        cooldown = 2;
    }

    public void KurangiCooldown()
    {
        if (cooldown > 0)
        {
            cooldown -= 1;
        }
    }

    public bool KeteranganCooldown()
    {
        return cooldown > 0;
    }
}

// Implementasi kemampuan Serangan Listrik
public class SeranganListrik : IKemampuan
{
    int cooldown;

    public void Gunakan(Robot pengguna, Robot target)
    {
        if (KeteranganCooldown())
        {
            Console.WriteLine("Kemampuan Serangan Listrik sedang cooldown!");
            return;
        }

        // Lakukan serangan listrik
        int seranganListrik = 20;
        target.Energi -= seranganListrik;
        cooldown = 3;
        Console.WriteLine($"Robot {pengguna.Nama} menggunakan Serangan Listrik pada {target.Nama}, mengurangi {seranganListrik} energi.");
    }

    public void KurangiCooldown()
    {
        if (cooldown > 0)
        {
            cooldown -= 1;
        }
    }

    public bool KeteranganCooldown()
    {
        return cooldown > 0;
    }
}

// Implementasi kemampuan Serangan Plasma
public class SeranganPlasma : IKemampuan
{
    int cooldown;

    public void Gunakan(Robot pengguna, Robot target)
    {
        int SeranganPlasma = 20;
        if (cooldown > 0)
        {
            Console.WriteLine("Kemampuan Serangan Plasma Sedang Cooldown!");
            return;
        }
        else if (SeranganPlasma >= target.Energi)
        {
            // Jika serangan plasma lebih besar dari energi target, langsung matikan
            target.Energi = 0;
            Console.WriteLine($"Robot {pengguna.Nama} Menembakkan Plasma Cannon! pada {target.Nama}");
            Console.WriteLine($"Robot {target.Nama} telah terbunuh karena kehabisan energi disebabkan oleh Plasma Cannon.");
        }
        else
        {
            // Kurangi energi target
            target.Energi -= SeranganPlasma;
            Console.WriteLine($"Robot {pengguna.Nama} Menembakkan Plasma Cannon! pada {target.Nama}, mengurangi energi sebesar {SeranganPlasma}.");
        }
        cooldown = 2;
    }

    public void KurangiCooldown()
    {
        if (cooldown > 0)
        {
            cooldown -= 1;
        }
    }

    public bool KeteranganCooldown()
    {
        return (cooldown > 0);
    }
}

// Implementasi kemampuan Pertahanan Super
public class PertahananSuper : IKemampuan
{
    int cooldown;

    public void Gunakan(Robot pengguna, Robot target)
    {
        if (cooldown > 0)
        {
            Console.WriteLine("Kemampuan Pertahanan Super Sedang Cooldown!");
            return;
        }
        // Aktifkan pertahanan super
        Console.WriteLine("Robot sedang melakukan Pertahanan Super.");
        pengguna.Armor += 20;
        cooldown = 2;
    }

    public void KurangiCooldown()
    {
        if (cooldown > 0)
        {
            cooldown -= 1;
        }
    }

    public bool KeteranganCooldown()
    {
        return (cooldown > 0);
    }
}

// Kelas untuk robot biasa
public class RobotBiasa : Robot
{
    public RobotBiasa(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan) { }

    public override void GunakanKemampuan(IKemampuan kemampuan, Robot target)
    {
        kemampuan.Gunakan(this, target);
    }
}

// Kelas untuk bos robot
public class BosRobot : Robot
{
    public BosRobot(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan) { }

    public override void GunakanKemampuan(IKemampuan kemampuan, Robot target)
    {
        kemampuan.Gunakan(this, target);
    }

    // Method khusus untuk bos robot ketika diserang
    public void Diserang(Robot penyerang)
    {
        int seranganYangDiterima = penyerang.Serangan;
        if (Armor > 0)
        {
            if (seranganYangDiterima >= Armor)
            {
                seranganYangDiterima -= Armor;
                Armor = 0;
                Console.WriteLine($"{penyerang.Nama} menghancurkan armor {Nama}.");
            }
            else
            {
                Armor -= seranganYangDiterima;
                seranganYangDiterima = 0;
                Console.WriteLine($"{penyerang.Nama} mengurangi armor {Nama} sebanyak {penyerang.Serangan}.");
            }
        }

        if (seranganYangDiterima > 0)
        {
            Energi -= seranganYangDiterima;
            if (Energi < 0)
            {
                Energi = 0;
            }
            Console.WriteLine($"{penyerang.Nama} menyerang {Nama}, mengurangi energi sebesar {seranganYangDiterima}.");
        }

        if (Energi <= 0)
        {
            Mati();
        }
    }

    // Method ketika bos robot mati
    public void Mati()
    {
        Console.WriteLine($"Bos {Nama} telah mati.");
    }
}

// Kelas utama untuk mengelola permainan
public class Game
{
    private List<Robot> robots;
    private BosRobot bosRobot;
    private List<IKemampuan> kemampuan;

    public Game()
    {
        // Inisialisasi robot-robot dalam permainan
        robots = new List<Robot>
        {
            new RobotBiasa("Optimus Prime", 125, 50, 25),
            new RobotBiasa("Ochobot", 100, 60, 30)
        };
        bosRobot = new BosRobot("Megatron", 200, 100, 75);

        // Inisialisasi kemampuan-kemampuan
        kemampuan = new List<IKemampuan>
        {
            new SeranganListrik(),
            new SeranganPlasma(),
            new Perbaikan(),
            new PertahananSuper()
        };
    }

    // Method utama untuk menjalankan permainan
    public void JalankanPermainan()
    {
        while (true)
        {
            // Tampilkan menu utama
            Console.WriteLine("\n=== Pertarungan Robot ===");
            Console.WriteLine("1. Robot1 menyerang Bos");
            Console.WriteLine("2. Robot2 menyerang Bos");
            Console.WriteLine("3. Gunakan kemampuan khusus pada Bos");
            Console.WriteLine("4. Cetak informasi robot");
            Console.WriteLine("5. Keluar");
            Console.Write("Pilih opsi: ");

            string pilihan = Console.ReadLine();
            switch (pilihan)
            {
                case "1":
                    robots[0].Serang(bosRobot);
                    break;
                case "2":
                    robots[1].Serang(bosRobot);
                    break;
                case "3":
                    GunakanKemampuan();
                    break;
                case "4":
                    Console.WriteLine("\nInformasi Robot:");
                    foreach (var robot in robots)
                    {
                        robot.CetakInformasi();
                    }
                    bosRobot.CetakInformasi();
                    break;
                case "5":
                    Console.WriteLine("Keluar dari program.");
                    return;
                default:
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                    break;
            }

            // Cek kondisi kemenangan atau kekalahan
            if (bosRobot.Energi <= 0)
            {
                Console.WriteLine("Selamat! Para robot berhasil mengalahkan Bos Robot!");
                return;
            }
            else if (robots.All(r => r.Energi <= 0))
            {
                Console.WriteLine("Game Over! Bos Robot berhasil mengalahkan semua robot.");
                return;
            }

            // Kurangi cooldown kemampuan setiap giliran
            foreach (var k in kemampuan)
            {
                k.KurangiCooldown();
            }
        }
    }

    // Method untuk menggunakan kemampuan
    private void GunakanKemampuan()
    {
        // Tampilkan pilihan kemampuan
        Console.WriteLine("\nPilih kemampuan yang akan digunakan:");
        Console.WriteLine("1. Serangan Listrik");
        Console.WriteLine("2. Serangan Plasma");
        Console.WriteLine("3. Perbaikan");
        Console.WriteLine("4. Pertahanan Super");
        Console.Write("Masukkan pilihan kemampuan: ");

        string pilihanKemampuan = Console.ReadLine();

        // Tampilkan pilihan robot
        Console.WriteLine("\nPilih robot yang akan menggunakan kemampuan:");
        Console.WriteLine("1. Robot1");
        Console.WriteLine("2. Robot2");
        Console.Write("Masukkan pilihan robot: ");

        string pilihanRobot = Console.ReadLine();
        Robot pengguna;

        // Pilih robot berdasarkan input
        if (pilihanRobot == "1")
        {
            pengguna = robots[0];
        }
        else if (pilihanRobot == "2")
        {
            pengguna = robots[1];
        }
        else
        {
            Console.WriteLine("Pilihan robot tidak valid.");
            return;
        }

        // Gunakan kemampuan berdasarkan pilihan
        switch (pilihanKemampuan)
        {
            case "1":
                pengguna.GunakanKemampuan(kemampuan[0], bosRobot);
                break;
            case "2":
                pengguna.GunakanKemampuan(kemampuan[1], bosRobot);
                break;
            case "3":
                pengguna.GunakanKemampuan(kemampuan[2], pengguna);
                break;
            case "4":
                pengguna.GunakanKemampuan(kemampuan[3], pengguna);
                break;
            default:
                Console.WriteLine("Pilihan kemampuan tidak valid.");
                break;
        }
    }
}

// Kelas Program sebagai entry point
class Program
{
    static void Main(string[] args)
    {
        // Buat instance Game dan jalankan permainan
        Game game = new Game();
        game.JalankanPermainan();
    }
}
