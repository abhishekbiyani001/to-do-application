using System;
using System.Collections.Generic;

namespace todo
{
    public class Task
    {
        required public string Description
        {
            get;
            set;
        }
        required public string Priority
        {
            get;
            set;
        }
        public DateTime DueDate
        {
            get;
            set;
        }
    }

    class Program
    {
        public List<Task> Tasks = new List<Task>();
        public List<string> PriorityLevels = new List<string> { "High", "Medium", "Low" };

        public void AddTask(string description, string priority, DateTime? dueDate)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Task description cannot be empty!");
                return;
            }

            DateTime finalDueDate = dueDate ?? DateTime.MaxValue;

            Tasks.Add(new Task
            {
                Description = description,
                Priority = priority,
                DueDate = finalDueDate
            });

            Console.WriteLine("Task successfully added.");
            Console.WriteLine();
        }
        public void viewTasks()
        {
            Console.WriteLine();
            Console.WriteLine("***************************************************");
            if (Tasks.Count > 0)
            {
                Console.WriteLine("Overdue or Upcoming Tasks:");
                foreach (var task in Tasks)
                {
                    if (task.DueDate < DateTime.Now)
                    {
                        Console.WriteLine($"[Overdue] {task.Description} - Priority: {task.Priority}, Due: {task.DueDate.ToShortDateString()}");
                    }
                    else if (task.DueDate != DateTime.MaxValue && (task.DueDate - DateTime.Now).TotalDays <= 1)
                    {
                        Console.WriteLine($"[Due soon] {task.Description} - Priority: {task.Priority}, Due: {task.DueDate.ToShortDateString()}");
                    }
                }

                Console.WriteLine("\nAll Tasks:");
                int index = 1;
                foreach (var task in Tasks)
                {
                    string dueDateDisplay = task.DueDate == DateTime.MaxValue ? "No Due Date" : task.DueDate.ToShortDateString();
                    Console.WriteLine($"{index}. {task.Description} - Priority: {task.Priority}, Due: {dueDateDisplay}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine("NO NEW TASKS!");
            }
            Console.WriteLine("***************************************************");
            Console.WriteLine();
        }

        public void deleteTask(int taskNumber)
        {
            if (taskNumber < 1 || taskNumber > Tasks.Count)
            {
                Console.WriteLine("Invalid task number. Please enter a valid task number to delete a task.");
                return;
            }

            Tasks.RemoveAt(taskNumber - 1);
            Console.WriteLine($"Task number {taskNumber} has been deleted successfully.");
        }

        static void Main()
        {
            Program p = new();
            int choice;

            do
            {
                try
                {
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("1. Add a new task.");
                    Console.WriteLine("2. Delete a task.");
                    Console.WriteLine("3. View all tasks.");
                    Console.WriteLine("4. Quit the application.");
                    choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1)
                    {
                        Console.Write("Enter a task: ");
                        string description = Console.ReadLine()!;

                        string priority = "";
                        while (true)
                        {
                            Console.Write("Enter priority (High, Medium, Low) or press Enter to skip: ");
                            string priorityInput = Console.ReadLine()!;

                            if (string.IsNullOrWhiteSpace(priorityInput))
                            {
                                break;
                            }

                            if (p.PriorityLevels.Contains(priorityInput, StringComparer.OrdinalIgnoreCase))
                            {
                                priority = char.ToUpper(priorityInput[0]) + priorityInput.Substring(1).ToLower();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid priority level. Please enter High, Medium, or Low.");
                            }
                        }

                        DateTime? dueDate = null;
                        while (true)
                        {
                            Console.Write("Enter due date (yyyy-mm-dd) or press Enter to skip: ");
                            string dueDateInput = Console.ReadLine()!;

                            if (string.IsNullOrWhiteSpace(dueDateInput))
                            {
                                break;
                            }

                            if (DateTime.TryParse(dueDateInput, out DateTime parsedDate))
                            {
                                dueDate = parsedDate;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format. Please try again.");
                            }
                        }
                        p.AddTask(description, priority, dueDate);
                    }
                    else if (choice == 2)
                    {
                        Console.Write("Enter the task number to delete: ");
                        int taskNumber = Convert.ToInt32(Console.ReadLine());
                        p.deleteTask(taskNumber);
                    }
                    else if (choice == 3)
                    {
                        p.viewTasks();
                    }
                    else if (choice == 4)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please try again.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }

                Console.WriteLine();
            } while (true);

            Console.WriteLine("You have successfully quit the application.");
        }
    }
}
