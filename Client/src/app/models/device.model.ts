export interface Device {
  id: string;
  name: string;
  manufacturer: string;
  type: string;
  operatingSystem: string;
  osVersion: string;
  processor: string;
  ram: number;
  description: string;
  assignedUserId: string | null;
}