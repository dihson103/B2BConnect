import { NextRequest, NextResponse } from 'next/server'

export async function GET(request: NextRequest) {
  console.log('>tesst')
  return NextResponse.redirect(new URL('/logout', request.url))
}
