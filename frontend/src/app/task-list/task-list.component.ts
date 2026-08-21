import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../services/task.service';
import { TaskItem } from '../models/task.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  tasks: TaskItem[] = [];
  newTask: TaskItem = {
    title: '',
    description: '',
    status: 'Pending',
    userId: 1
  };

  constructor(private taskService: TaskService, private router: Router) {}

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

  deleteTask(id?: number) {
    if (!id) return;

    this.taskService.deleteTask(id).subscribe({
      next: () => {
        this.tasks = this.tasks.filter(task => task.id !== id);
      },
      error: (err) => {
        console.error('Error al eliminar la tarea:', err);
      }
    });
  }

  toggleTaskStatus(task: TaskItem) {
    task.status = task.status === 'Completed' ? 'Pending' : 'Completed';
    
    const taskToUpdate = {
      ...task,
      userId: task.userId || 1 
    };

    this.taskService.updateTask(taskToUpdate).subscribe({
      next: () => {
        console.log('Tarea actualizada correctamente');
      },
      error: (err) => {
        console.error('Error al actualizar la tarea:', err);
      }
    });
  }

  logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}