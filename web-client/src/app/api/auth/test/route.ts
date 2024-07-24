import { cookies } from 'next/headers'
import { redirect } from 'next/navigation'
import { NextResponse } from 'next/server'

export async function GET(request: Request) {
  const token = cookies().get('loginResponse')?.value
  cookies().delete('loginResponse')
  console.log('>>>>>>>>>>>>>>> aaaaaaaaaaaaaaaaaaaaaaaa', token)
  return Response.json({ message: 'Đăng xuất thành công' })
  // return NextResponse.redirect(new URL('/login', request.url))
}
