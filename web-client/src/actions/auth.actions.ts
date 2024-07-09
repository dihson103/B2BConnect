'use server'

import http from '@/lib/http'
import { cookies } from 'next/headers'
import { LoginSuccessResponse, LoginType, RegisterSuccessResponse, RegisterType } from '@/types/auth.types'

export const loginAction = async (body: LoginType) => {
  let response;
  try {
    response = await http.post<LoginSuccessResponse>('/auth/login', body)
    if (response.status === 200 && response.data) {
      cookies().set('userId', response.data.account.id);
      cookies().set('email', response.data.account.email);
      cookies().set('accessToken', response.data.accessToken);
      cookies().set('refreshToken', response.data.refreshToken);
    } else {
      throw new Error('Login failed: Unexpected response');
    }
  } catch (error) {
    console.error('Login request failed:', error);
    throw error;
}
  // set cookie 
  cookies().set('userId', response.data.account.id)
  cookies().set('email', response.data.account.email)
  cookies().set('accessToken', response.data.accessToken)
  cookies().set('refreshToken', response.data.refreshToken)
  return response;
}

export const registerAction = (body: RegisterType) => {
  return http.post<RegisterSuccessResponse>('/auth/register', body)
}

export const logoutAction = () => {
  const userIdCookie = cookies().get('userId')
  const userId = userIdCookie ? userIdCookie.value : null;
  console.log('userId = ' + userId);
  if (userId) {
    http.delete<LoginSuccessResponse>(`/auth/logout/${userId}`)
    .then(response => {
      // remove cookie
      cookies().delete('userId')
      cookies().delete('email')
      cookies().delete('accessToken')
      cookies().delete('refreshToken')
    })
  } else {
    console.error('User ID not found in cookies');
  }
}
