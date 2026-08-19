import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskService } from './services/task.service';
import { TaskItem } from './models/task.model';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App implements OnInit {
  tasks: TaskItem[] = [];
  newTask: TaskItem = {
    title: '',
    description: '',
    status: 'Pending',
    userId: 1
  };

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.getTasks().subscribe({
      next: (data) => (this.tasks = data),
      error: (err) => console.error('Error al cargar tareas:', err)
    });
  }

  addTask() {
    if (!this.newTask.title.trim()) return;

    this.taskService.createTask(this.newTask).subscribe({
      next: (created) => {
        this.tasks.push(created);
        this.newTask = { title: '', description: '', status: 'Pending', userId: 1 };
      },
      error: (err) => console.error('Error al crear tarea:', err)
    });
  }
}