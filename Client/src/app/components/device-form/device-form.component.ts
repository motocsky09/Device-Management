import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Device } from '../../models/device.model';
import { User } from '../../models/user.model';
import { DeviceService } from '../../services/device.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-device-form',
  templateUrl: './device-form.component.html',
  styleUrls: ['./device-form.component.css']
})
export class DeviceFormComponent implements OnInit {
  isEditMode = false;
  device: Device = {
    id: '',
    name: '',
    manufacturer: '',
    type: '',
    operatingSystem: '',
    osVersion: '',
    processor: '',
    ram: 0,
    description: '',
    assignedUserId: null
  };
  users: User[] = [];
  errorMessage = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private deviceService: DeviceService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.userService.getUsers().subscribe(users => {
      this.users = users;
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.deviceService.getDeviceById(id).subscribe(device => {
        this.device = device;
      });
    }
  }

  save(): void {
    // Validare - toate campurile obligatorii
    if (!this.device.name || !this.device.manufacturer || !this.device.type ||
        !this.device.operatingSystem || !this.device.osVersion ||
        !this.device.processor || !this.device.ram) {
      this.errorMessage = 'All fields are required.';
      return;
    }

    if (this.isEditMode) {
      this.deviceService.updateDevice(this.device).subscribe(() => {
        this.router.navigate(['/devices']);
      });
    } else {
      // Validare - device-ul nu exista deja
      this.deviceService.getDevices().subscribe(devices => {
        const exists = devices.some(d =>
          d.name.toLowerCase() === this.device.name.toLowerCase() &&
          d.manufacturer.toLowerCase() === this.device.manufacturer.toLowerCase()
        );
        if (exists) {
          this.errorMessage = 'A device with this name and manufacturer already exists.';
          return;
        }
        this.deviceService.createDevice(this.device).subscribe(() => {
          this.router.navigate(['/devices']);
        });
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/devices']);
  }
}