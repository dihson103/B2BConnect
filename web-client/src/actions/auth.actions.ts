'use server'

import http from '@/lib/http'
import { cookies } from 'next/headers'
import { LoginResponse, LoginSuccessResponse, LoginType, RegisterSuccessResponse, RegisterType } from '@/types/auth.types'

export const loginAction = async (body: LoginType) => {
  let response;
  try {
    response = await http.post<LoginSuccessResponse>('/auth/login', body)
    if (response.status === 200 && response.data) {
      cookies().set({
        name: "loginResponse", 
        value: JSON.stringify(response.data),
        httpOnly: true
      })
    } else {
      throw new Error('Login failed: Unexpected response');
    }
  } catch (error) {
    console.error('Login request failed:', error)
    throw error
}
  return response
}

export const registerAction = (body: RegisterType) => {
  return http.post<RegisterSuccessResponse>('/auth/register', body)
}

export const logoutAction = () => {
  const loginResponse = getLoginResponseCookie();
  const userId = loginResponse?.account?.id; 
  console.log('userId = ' + userId);
  if (userId) {
    http.delete<LoginSuccessResponse>(`/auth/logout/${userId}`)
    .then(response => {
      cookies().delete('loginResponse');
    })
  } else {
    console.error('User ID not found in cookies')
  }
}

export const getLoginResponseCookie = () : LoginResponse | null => {
  try {
    const loginResponse =  cookies().get('loginResponse')?.value;
    if (!loginResponse) {
      return null;
    }
    return JSON.parse(loginResponse);
  } catch (err) {
    console.error('Error parsing loginResponse cookie:', err);
    return null;
  }
}
