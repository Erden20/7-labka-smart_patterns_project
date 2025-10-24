using System;
using System.Collections.Generic;

interface ICommand
{
    void Execute();
    void Undo();
}

class Light
{
    public void On() => Console.WriteLine("💡 Свет включен");
    public void Off() => Console.WriteLine("💡 Свет выключен");
}

class Door
{
    public void Open() => Console.WriteLine("🚪 Дверь открыта");
    public void Close() => Console.WriteLine("🚪 Дверь закрыта");
}

class Thermostat
{
    private int temperature = 22;
    public void Increase()
    {
        temperature++;
        Console.WriteLine($"🌡 Температура увеличена до {temperature}°C");
    }
    public void Decrease()
    {
        temperature--;
        Console.WriteLine($"🌡 Температура уменьшена до {temperature}°C");
    }
}

class LightOnCommand : ICommand
{
    private Light light;
    public LightOnCommand(Light light) { this.light = light; }
    public void Execute() => light.On();
    public void Undo() => light.Off();
}

class LightOffCommand : ICommand
{
    private Light light;
    public LightOffCommand(Light light) { this.light = light; }
    public void Execute() => light.Off();
    public void Undo() => light.On();
}

class DoorOpenCommand : ICommand
{
    private Door door;
    public DoorOpenCommand(Door door) { this.door = door; }
    public void Execute() => door.Open();
    public void Undo() => door.Close();
}

class DoorCloseCommand : ICommand
{
    private Door door;
    public DoorCloseCommand(Door door) { this.door = door; }
    public void Execute() => door.Close();
    public void Undo() => door.Open();
}

class IncreaseTempCommand : ICommand
{
    private Thermostat thermostat;
    public IncreaseTempCommand(Thermostat t) { thermostat = t; }
    public void Execute() => thermostat.Increase();
    public void Undo() => thermostat.Decrease();
}

class DecreaseTempCommand : ICommand
{
    private Thermostat thermostat;
    public DecreaseTempCommand(Thermostat t) { thermostat = t; }
    public void Execute() => thermostat.Decrease();
    public void Undo() => thermostat.Increase();
}

class SmartHomeInvoker
{
    private Stack<ICommand> history = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        history.Push(command);
    }

    public void UndoCommand()
    {
        if (history.Count > 0)
        {
            ICommand last = history.Pop();
            last.Undo();
        }
        else
        {
            Console.WriteLine("⚠️ Нет команд для отмены");
        }
    }
}

class Program
{
    static void Main()
    {
        Light light = new Light();
        Door door = new Door();
        Thermostat thermostat = new Thermostat();
        SmartHomeInvoker smartHome = new SmartHomeInvoker();

        Console.WriteLine("=== Управление умным домом ===");

        smartHome.ExecuteCommand(new LightOnCommand(light));
        smartHome.ExecuteCommand(new DoorOpenCommand(door));
        smartHome.ExecuteCommand(new IncreaseTempCommand(thermostat));

        Console.WriteLine("\n--- Отмена последних команд ---");
        smartHome.UndoCommand();
        smartHome.UndoCommand();
        smartHome.UndoCommand();

        Console.WriteLine("\n✅ Пример завершён.");
    }
}
