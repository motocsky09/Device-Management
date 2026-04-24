export interface RegisterRequest {
  name: string;
  email: string;
  password: string;
  location: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  userId: string;
  userName: string;
}