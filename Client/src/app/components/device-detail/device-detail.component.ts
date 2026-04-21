import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Device } from '../../models/device.model';
import { User } from '../../models/user.model';
import { DeviceService } from '../../services/device.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-device-detail',
  templateUrl: './device-detail.component.html',
  styleUrls: ['./device-detail.component.css']
})
export class DeviceDetailComponent implements OnInit {
  device: Device | null = null;
  assignedUser: User | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private deviceService: DeviceService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.deviceService.getDeviceById(id).subscribe(device => {
        this.device = device;
        if (device.assignedUserId) {
          this.userService.getUserById(device.assignedUserId).subscribe(user => {
            this.assignedUser = user;
          });
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/devices']);
  }

  editDevice(): void {
    this.router.navigate(['/devices/edit', this.device?.id]);
  }
}