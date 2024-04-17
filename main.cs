using System;
using System.Net.Sockets;
using NModbus;

class Program
{
    static void Main()
    {
        // Địa chỉ IP và cổng của robot Modbus TCP/IP
        string ipAddress = "192.168.1.100";
        int port = 502;

        // Tạo kết nối TCP đến robot Modbus
        TcpClient client = new TcpClient(ipAddress, port);

        // Tạo đối tượng Modbus TCP/IP
        ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

        // Địa chỉ slave của robot Modbus
        byte slaveId = 1;

        try
        {
            // Đọc giá trị từ thanh ghi holding registers của robot
            ushort startAddress = 0;
            ushort numRegistersToRead = 10;
            ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegistersToRead);

            // Hiển thị các giá trị đọc được
            Console.WriteLine("Read values:");
            for (int i = 0; i < numRegistersToRead; i++)
            {
                Console.WriteLine($"Register {startAddress + i}: {registers[i]}");
            }

            // Ghi giá trị vào thanh ghi holding registers của robot (nếu cần)
            ushort registerAddressToWrite = 0;
            ushort valueToWrite = 123;
            master.WriteSingleRegister(slaveId, registerAddressToWrite, valueToWrite);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            // Đóng kết nối TCP
            client.Close();
        }
    }
}
