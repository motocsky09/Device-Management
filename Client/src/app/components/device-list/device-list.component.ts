import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Device } from '../../models/device.model';
import { User } from '../../models/user.model';
import { DeviceService } from '../../services/device.service';
import { UserService } from '../../services/user.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-device-list',
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.css']
})
export class DeviceListComponent implements OnInit {
  devices: Device[] = [];
  users: User[] = [];
  currentUserId: string | null = null;
  currentUserName: string | null = null;

  constructor(
    private deviceService: DeviceService,
    private userService: UserService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.currentUserId = this.authService.getUserId();
    this.currentUserName = this.authService.getUserName();
    this.loadDevices();
    this.loadUsers();
  }

  loadDevices(): void {
    this.deviceService.getDevices().subscribe(data => {
      this.devices = data;
    });
  }

  loadUsers(): void {
    this.userService.getUsers().subscribe(data => {
      this.users = data;
    });
  }

  getUserName(userId: string | null): string {
    if (!userId) return 'Unassigned';
    const user = this.users.find(u => u.id === userId);
    return user ? user.name : 'Unknown';
  }

  viewDetails(id: string): void {
    this.router.navigate(['/devices', id]);
  }

  editDevice(id: string): void {
    this.router.navigate(['/devices/edit', id]);
  }

  deleteDevice(id: string): void {
    if (confirm('Are you sure you want to delete this device?')) {
      this.deviceService.deleteDevice(id).subscribe(() => {
        this.loadDevices();
      });
    }
  }

  createDevice(): void {
    this.router.navigate(['/devices/new']);
  }

  assignDevice(deviceId: string): void {
    if (!this.currentUserId) return;
    this.deviceService.assignDevice(deviceId, this.currentUserId).subscribe(() => {
      this.loadDevices();
    });
  }

  unassignDevice(deviceId: string): void {
    if (!this.currentUserId) return;
    this.deviceService.unassignDevice(deviceId, this.currentUserId).subscribe(() => {
      this.loadDevices();
    });
  }

  isAssignedToMe(device: Device): boolean {
    return device.assignedUserId === this.currentUserId;
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}