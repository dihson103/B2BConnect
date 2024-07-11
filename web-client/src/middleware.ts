import envConfig from '@/config'
import { cookies } from 'next/headers'
import { NextResponse } from 'next/server'
import { LoginResponse } from './types/auth.types'
import { getLoginResponseCookie } from './actions/auth.actions'

export default async function middleware(request: Request) {
  if (request.url === `${envConfig.BASE_URL}/`) {
    return NextResponse.redirect(envConfig.HOME_URL)
  }
  if (request.url === envConfig.ADMIN_URL) {
    return NextResponse.redirect(envConfig.DASHBOARD_URL)
  }
  const loginResponse = await getLoginResponseCookie();
  const isAdmin = loginResponse?.account.isAdmin
  // if (loginResponse === null) {
  //   if (request.url !== `${envConfig.LOGIN_URL}` || request.url !== `${envConfig.REGISTER_URL}`) {
  //     return NextResponse.redirect(envConfig.LOGIN_URL)
  //   }
  // } else {
  //   if (request.url === `${envConfig.LOGIN_URL}` || request.url === `${envConfig.REGISTER_URL}`) {
  //     // TODO: Handle redirect for Admin vs User
  //     return NextResponse.redirect(envConfig.HOME_URL)
  //   }
  // }
  
  if (!isAdmin) {
    // TODO: Handle redirect for User wanting to access Admin-only pages 
  }

  return NextResponse.next()
}
