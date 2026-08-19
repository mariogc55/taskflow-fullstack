export interface TaskItem {
  id?: number;
  title: string;
  description: string;
  status: string;
  createdAt?: string;
  userId: number;
}